using LoadingSystem.BusinessLogic.Enums;
using LoadingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace LoadingSystem.BusinessLogic.Manager
{
    public class CardManager
    {
        private LoadingSystemEntities _db;
        private DateTime _currentDateTime;

        public CardManager()
        {
            this._db = new LoadingSystemEntities();
            this._currentDateTime = DateTime.Now;
        }

        public Card BuyCard()
        {
            var existingNumbers = this._db.Cards.Select(x => x.CardNo).Distinct().ToList();
            var cardNum = this.GenerateRandomGvNumber(existingNumbers);

            var cardInfo = new Card
            {
                CardNo = cardNum,
                CardType = (int)CardType.Regular,
                PurchaseDate = this._currentDateTime,
                ExpiryDate = this._currentDateTime.AddYears(5).AddDays(1),
                DiscountType = (int)DiscountType.None,
                Amount = 100,
                SeniorCitizenControlNumber = "",
                PwdId = ""
            };

            this._db.Cards.Add(cardInfo);
            this._db.SaveChanges();

            return cardInfo;
        }

        public Card LoadCard(string cardNumber, decimal tenderedAmount, decimal cash)
        {
            var cardInfo = this._db.Cards.FirstOrDefault(x => x.CardNo == cardNumber);
            if (cardInfo == null)
            {
                throw new Exception("Card details cannot be found.");
            }
            if (cardInfo.ExpiryDate.Value < DateTime.Now.Date)
            {
                throw new Exception("Card is already expired.");
            }
            if (tenderedAmount < 100 && tenderedAmount > 10000)
            {
                throw new Exception("Please enter amount 100 up to 10000.");
            }
            if (cash < tenderedAmount)
            {
                throw new Exception("Cash is insufficient to your load amount");
            }

            var cardLoadHistory = new CardLoadHistory
            {
                ProcessDateAndTime = this._currentDateTime,
                CardId = cardInfo.CardId,
                TenderedAmount = tenderedAmount,
                Cash = cash,
                Changed = cash - tenderedAmount
            };
            this._db.CardLoadHistories.Add(cardLoadHistory);

            cardInfo.Amount += tenderedAmount;

            this._db.SaveChanges();

            return cardInfo;
        }

        public Card ValidateCard(string cardNumber)
        {
            var cardInfo = this._db.Cards.FirstOrDefault(x => x.CardNo == cardNumber);
            if (cardInfo == null)
            {
                throw new Exception("Card details cannot be found.");
            }
            if (cardInfo.ExpiryDate.Value < this._currentDateTime)
            {
                throw new Exception("Card is already expired.");
            }
            var cardCount = this._db.CardTravelHistories
                .Where(x =>
                    x.CardId == cardInfo.CardId &&
                    DbFunctions.TruncateTime(x.ProcessDateAndTime) == DbFunctions.TruncateTime(this._currentDateTime))
                .Count();

            cardInfo.TodayDiscountCount = cardCount;

            return cardInfo;
        }

        public Card GetCardById(int cardId)
        {
            var cardInfo = this._db.Cards.FirstOrDefault(x => x.CardId == cardId);
            if (cardInfo == null)
            {
                throw new Exception("Card details cannot be found.");
            }
            if (cardInfo.ExpiryDate.Value < this._currentDateTime)
            {
                throw new Exception("Card is already expired.");
            }

            var cardCount = this._db.CardTravelHistories
                .Where(x => 
                    x.CardId == cardId && 
                    DbFunctions.TruncateTime(x.ProcessDateAndTime) == DbFunctions.TruncateTime(this._currentDateTime))
                .Count();

            cardInfo.TodayDiscountCount = cardCount;

            return cardInfo;
        }

        public Card ValidateCardForDiscount(string cardNumber)
        {
            var cardInfo = this._db.Cards.FirstOrDefault(x => x.CardNo == cardNumber);
            if (cardInfo == null)
            {
                throw new Exception("Card details cannot be found.");
            }
            if (cardInfo.ExpiryDate.Value < DateTime.Now.Date)
            {
                throw new Exception("Card is already expired.");
            }
            if (((this._currentDateTime - cardInfo.PurchaseDate.Value).TotalDays / 30) >= 6)
            {
                throw new Exception("Discount registration must not exceeds 6 months after purchased.");
            }
            if (cardInfo.CardType != null && cardInfo.CardType.Value != (int)CardType.Regular)
            {
                var cardTypeName = cardInfo.CardType == (int)CardType.Senior ? "Senior Citizen" : "PWD";
                throw new Exception("Card already registered to " + cardTypeName);
            }

            var cardCount = this._db.CardTravelHistories
                .Where(x =>
                    x.CardId == cardInfo.CardId &&
                    DbFunctions.TruncateTime(x.ProcessDateAndTime) == DbFunctions.TruncateTime(this._currentDateTime))
                .Count();

            cardInfo.TodayDiscountCount = cardCount;

            return cardInfo;
        }

        public Card RegisterCardToDiscountType(string cardNumber, DiscountType discountType, string idNumber)
        {
            var cardInfo = this._db.Cards.FirstOrDefault(x => x.CardNo == cardNumber);
            if (cardInfo == null)
            {
                throw new Exception("Card details cannot be found.");
            }
            if (cardInfo.ExpiryDate.Value < DateTime.Now.Date)
            {
                throw new Exception("Card is already expired.");
            }
            if (((this._currentDateTime - cardInfo.PurchaseDate.Value).TotalDays / 30) >= 6)
            {
                throw new Exception("Discount registration must not exceeds 6 months after purchased.");
            }
            if (cardInfo.CardType != null && cardInfo.CardType.Value != (int)CardType.Regular)
            {
                var cardTypeName = cardInfo.CardType == (int)CardType.Senior ? "Senior Citizen" : "PWD";
                throw new Exception("Card already registered to " + cardTypeName);
            }

            if (discountType == DiscountType.Senior)
            {
                if (idNumber.Length != 10)
                {
                    throw new Exception("Invalid Senior Citizen Control Number.");
                }
                    cardInfo.CardType = (int)CardType.Senior;
                    cardInfo.DiscountType = (int)discountType;
                    cardInfo.SeniorCitizenControlNumber = idNumber;
                }

            if (discountType == DiscountType.Pwd)
            {
                if (idNumber.Length != 12)
                {
                    throw new Exception("Invalid PWD ID.");
                }
                cardInfo.CardType = (int)CardType.Pwd;
                cardInfo.DiscountType = (int)discountType;
                cardInfo.PwdId = idNumber;
            }

            this._db.SaveChanges();

            return cardInfo;
        }

        public List<Station> GetStationByLine(int line, SortType sortType)
        {
            var stations = this._db.Stations.Where(x => x.Group == line).ToList();
            if (stations == null)
            {
                throw new Exception("Stations cannot be found.");
            }

            if (sortType == SortType.Asc)
            {
                stations.OrderBy(x => x.Sequence);
            }
            else {
                stations.OrderByDescending(x => x.Sequence);
            }

            return stations;
        }

        public StationPrice GetStationPriceByOriginAndDestination(int originId, int destinationid)
        {
            var stationPrice = this._db.StationPrices.FirstOrDefault(x => x.StationOriginId == originId && x.StationDestinationId == destinationid);
            if (stationPrice == null)
            {
                stationPrice = this._db.StationPrices.FirstOrDefault(x => x.StationOriginId == destinationid && x.StationDestinationId == originId);
            }
            if (stationPrice == null)
            {
                throw new Exception("Travel fare to this destination is not yet setup.");
            }

            return stationPrice;
        }

        public Card UseCard(int cardId, int originStation, int destinationStation)
        {
            var card = this.GetCardById(cardId);
            var stationPrice = this.GetStationPriceByOriginAndDestination(originStation, destinationStation);
            if (card.Amount < stationPrice.Price)
            {
                throw new Exception("Insufficient Card Balance.");
            }

            decimal total = 0;
            decimal discount = 0;
            double discountP = 0.2;
            if (card != null && card.DiscountType != 0 && stationPrice != null)
            {
                discount =(stationPrice.Price ?? 0) * Convert.ToDecimal(discountP);
                total = (stationPrice.Price ?? 0) - discount;
            }
            else if (stationPrice != null)
            {
                total = (stationPrice.Price ?? 0);
            }

            if (card.TodayDiscountCount < 4)
            {
                discountP = 0.03;
                var discount1 = (stationPrice.Price ?? 0) * Convert.ToDecimal(discountP);
                total = total - discount1;
                discount += discount1;
            }

            var cardHistory = new CardTravelHistory
            {
                CardId = card.CardId,
                DestinationStation = destinationStation.ToString(),
                OriginStation = originStation.ToString(),
                DiscountAmount = discount,
                DiscountType = card.DiscountType,
                ProcessDateAndTime = this._currentDateTime,
                AmountPaid = total
            };
            this._db.CardTravelHistories.Add(cardHistory);
            card.Amount = card.Amount - total;
            _db.SaveChanges();
            return card;
        }


        public string GenerateRandomGvNumber(List<string> existingNumbers)
        {
            var rdm = new Random();

            var giftVoucherRandomCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var allowChrs = giftVoucherRandomCharacters.ToCharArray();
            var gvNumberLenght = 16;

            var sResult = "";
            var isAlreadyUse = false;
            long count = 0;
            do
            {
                // Generate Random Number
                for (var i = 0; i < gvNumberLenght; i++)
                {
                    sResult += allowChrs[rdm.Next(0, allowChrs.Length)];
                }

                //  Check GV if already exist
                var onDatabase = existingNumbers == null && existingNumbers.Count == 0 ? false : existingNumbers.Any(x => x == sResult);
                if (onDatabase)
                {
                    isAlreadyUse = true;
                }

                // Change Time
                Thread.Sleep(1000 * 2);

                // Max 16 Digit Combination 
                if (count == 10000000000000000)
                {
                    throw new ArgumentException("Cannot find maximum gift voucher number combination.");
                }
            }
            while (isAlreadyUse);

            return sResult;
        }
    }
}

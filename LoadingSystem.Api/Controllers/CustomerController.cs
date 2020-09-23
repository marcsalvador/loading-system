using LoadingSystem.BusinessLogic.Enums;
using LoadingSystem.BusinessLogic.Manager;
using LoadingSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LoadingSystem.Api.Controllers
{
    [RoutePrefix("customer")]
    public class CustomerController : ApiController
    {
        IHttpActionResult ExceptionHandler(Exception ex)
        {
            if (ex.InnerException != null) { return BadRequest(ex.Message + " - " + ex.InnerException.Message); }
            return BadRequest(ex.Message);
        }

        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("buy-card")]
        public IHttpActionResult BuyCard()
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.BuyCard();
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class LoadCardBindingModel
        {
            public string CardNumber { get; set; }
            public decimal TenderedAmount { get; set; }
            public decimal Cash { get; set; }
        }
        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("load-card")]
        public IHttpActionResult LoadCard([FromBody] LoadCardBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.LoadCard(model.CardNumber, model.TenderedAmount, model.Cash);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class ValidateCardBindingModel
        {
            public string CardNumber { get; set; }
        }
        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("validate-card")]
        public IHttpActionResult ValidateCard([FromBody] ValidateCardBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.ValidateCard(model.CardNumber);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("validate-card-discount")]
        public IHttpActionResult ValidateCardForDiscount([FromBody] ValidateCardBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.ValidateCardForDiscount(model.CardNumber);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class RegisterCardToDiscountTypeBindingModel
        {
            public string CardNumber { get; set; }
            public DiscountType DiscountType { get; set; }
            public string IdNumber { get; set; }
        }
        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("register-card")]
        public IHttpActionResult RegisterCardToDiscountType([FromBody] RegisterCardToDiscountTypeBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.RegisterCardToDiscountType(model.CardNumber, model.DiscountType, model.IdNumber);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class GetStationByLineBindingModel
        {
            public int Line { get; set; }
            public SortType SortType { get; set; }
        }
        [ResponseType(typeof(List<Station>))]
        [HttpPost]
        [AllowAnonymous]
        [Route("get-station-by-line")]
        public IHttpActionResult GetStationByLine([FromBody] GetStationByLineBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.GetStationByLine(model.Line, model.SortType);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class GetStationPriceByOriginAndDestinationByLineBindingModel
        {
            public int OriginId { get; set; }
            public int DestinationId { get; set; }
        }
        [ResponseType(typeof(StationPrice))]
        [HttpPost]
        [AllowAnonymous]
        [Route("get-station-fare")]
        public IHttpActionResult GetStationPriceByOriginAndDestination([FromBody] GetStationPriceByOriginAndDestinationByLineBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.GetStationPriceByOriginAndDestination(model.OriginId, model.DestinationId);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        public class UseCardLineBindingModel
        {
            public int CardId { get; set; }
            public int OriginId { get; set; }
            public int DestinationId { get; set; }
        }
        [ResponseType(typeof(Card))]
        [HttpPost]
        [AllowAnonymous]
        [Route("use-card")]
        public IHttpActionResult UseCard([FromBody] UseCardLineBindingModel model)
        {
            try
            {
                var cardManager = new CardManager();
                var card = cardManager.UseCard(model.CardId, model.OriginId, model.DestinationId);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }
    }
}

import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base';
import { FormControl } from '@angular/forms';
import { Card } from 'src/app/api/model';

@Component({
  selector: 'app-use-card',
  templateUrl: './use-card.component.html',
  styleUrls: ['./use-card.component.css']
})
export class UseCardComponent extends BaseComponent implements OnInit {

  public cardNumber = new FormControl('');
  public card: Card;
  public balance: number = 0;

  public line = new FormControl({ value: 0 });
  public lines: any[] = [];
  public stations: any[];
  public origin = new FormControl({ value: 0 });
  public destination = new FormControl({ value: 0 });
  public stationPrice: any;

  public total: number = 0;
  public discount: number = 0;

  ngOnInit(): void {
    this.lines.push({ value: '1', text: "MRT Line 1" });
    this.lines.push({ value: '2', text: "MRT Line 2" });
    this.origin.setValue(null);
    this.destination.setValue(null);
  }

  onLineChanged() {
    var model = {};
    if (this.line.value == "1") {
      model =
      {
        line: 1,
        sortType: 1
      }
    }
    else if (this.line.value == "2") {
      model =
      {
        line: 2,
        sortType: 1
      }
    }
    this._customerService.getStationByLine(model)
      .subscribe(
        result => {
          this.stations = result;
          this.origin.setValue(null);
          this.destination.setValue(null);
          this.stationPrice = null;
          this._spinner.hide();
        },
        error => {
          this._spinner.hide();
          this.showSnackbarMessage(error.json().message);
        }
      );
  }

  onStationChanged() {
    if (this.origin.value == NaN || this.origin.value == undefined || this.origin.value == null) {
      return;  
    }
    if (this.destination.value == NaN || this.destination.value == undefined || this.destination.value == null) {
      return;
    }

    var model =
    {
      originId: this.origin.value,
      destinationId: this.destination.value
    };
    this._customerService.getStationPriceByOriginAndDestination(model)
      .subscribe(
        result => {
          this.stationPrice = result;
          this.computeTotalFare();
          this._spinner.hide();
        },
        error => {
          this._spinner.hide();
          this.showSnackbarMessage(error.json().message);
        }
      );
  }

  computeTotalFare() {
    this.total = 0;
    this.discount = 0;
    if (this.card != null && this.card.discountType != 0 && this.stationPrice != null) {      
      this.discount = this.stationPrice.price * (20/100);
      this.total = this.stationPrice.price - this.discount;
    }
    else if(this.stationPrice != null)
    {
      this.total = this.stationPrice.price;
    }

    if (this.card != null && this.card.todayDiscountCount < 4)
    {
      console.log(1);
        var discountP = 0.03;
        var discount1 = (this.stationPrice.price ?? 0) * discountP;
        this.total = this.total - discount1;
        this.discount += discount1;
    }
  }

  back() {
    this._router.navigate([""]);
  }
  
  validate()
  {
    this._spinner.show();
    var model =
    {
        cardNumber: this.cardNumber.value
    }
    this._customerService.validateCard(model)
    .subscribe(
      result => {
        this._spinner.hide();
        this.card = result;
        this.balance = result.amount;
        this.computeTotalFare();
      },
      error => {
        this._spinner.hide();
        this.showSnackbarMessage(error.json().message);
      }
    );
  }

  contiue() {
    if (this.card == null) {
      this.showSnackbarMessage("Please enter and validate your card number.");
      return;
    }

    if (this.stationPrice == null) {
      this.showSnackbarMessage("Please select origin and destination station.");
      return;
    }
    
    this._spinner.show();    
    var model =
    {
      originId: this.origin.value,
      destinationId: this.destination.value,
      cardId: this.card.cardId
    };
    this._customerService.useCard(model)
      .subscribe(
        result => {
          this._spinner.hide();
          localStorage.setItem("qless-card", JSON.stringify(result));
          this._router.navigate(['successful']);
        },
        error => {
          this._spinner.hide();
          this.showSnackbarMessage(error.json().message);
        }
      );
  }
}

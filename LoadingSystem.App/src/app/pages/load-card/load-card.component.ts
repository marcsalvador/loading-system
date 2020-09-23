import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base';
import { timeout } from 'rxjs/operators';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-load-card',
  templateUrl: './load-card.component.html',
  styleUrls: ['./load-card.component.css']
})
export class LoadCardComponent extends BaseComponent implements OnInit {

  public tenderedAmount: number = 0;
  public cash = new FormControl({ value: 0 });
  public cardNumber = new FormControl('');
  public change: number = 0;
  public balance: number = 0;

  ngOnInit(): void {
    setTimeout(() => {
      this.clearCash();
    }, 100);
  }

  addAmount(value: number) { 
    var temp = this.tenderedAmount + value;
    if(temp<100)
    {
      this.showSnackbarMessage("Minimum load amount is 100.00");
      return;
    }
    if(temp > 10000)
    {
      this.showSnackbarMessage("Maximum load limit is 10000.00");
      return;
    }

    this.tenderedAmount = temp;
    this.onCashChange();
  }

  addCash(value: number) { 
    setTimeout(() => {
      var tmpCash = parseFloat(this.cash.value);
      if (tmpCash == NaN || tmpCash == undefined || tmpCash == null) {
        tmpCash = 0;  
      }
      tmpCash = tmpCash + value;
      this.cash.setValue(tmpCash);
      this.onCashChange();
    }, 100);
  }

  back()
  {
    this._router.navigate([""]);
  }

  clear()
  {
    this.tenderedAmount = 0;
    this.change = 0;
  }

  clearCash()
  {
    this.cash.setValue(0);
    this.change = 0;
  }

  onCashChange()
  {
    setTimeout(() => {
      var tmpCash = parseFloat(this.cash.value);
      if (tmpCash == NaN || tmpCash == undefined || tmpCash == null) {
        tmpCash = 0;
      }
      this.change = tmpCash - this.tenderedAmount;
    }, 100);
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
        this.balance = result.amount;
      },
      error => {
        this._spinner.hide();
        this.showSnackbarMessage(error.json().message);
      }
    );
  }
  
  contiue()
  {
    this._spinner.show();

    var tmpCash = this.cash.value;
    if (tmpCash == NaN || tmpCash == undefined || tmpCash == null) {
      tmpCash = 0;
    }
    if (tmpCash == 0) {
      tmpCash = this.tenderedAmount;
      this.change = tmpCash - this.tenderedAmount;
    }

    var model =
    {
        cardNumber: this.cardNumber.value,
        tenderedAmount: this.tenderedAmount,
        cash: tmpCash,
        change: this.change
    }
    this._customerService.loadCard(model)
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

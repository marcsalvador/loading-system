import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base';
import { FormControl } from '@angular/forms';
import { Card } from 'src/app/api/model';

@Component({
  selector: 'app-discount-registration',
  templateUrl: './discount-registration.component.html',
  styleUrls: ['./discount-registration.component.css']
})
export class DiscountRegistrationComponent extends BaseComponent implements OnInit {

  public cardNumber = new FormControl('');
  public card: Card;
  public balance: number = 0;

  public discountIdName: string = "";
  public idNumber = new FormControl('');
  public discountType = new FormControl({ value: 0 });
  public discountTypes: any[] = [];
  public max = 0;

  ngOnInit(): void {
    this.discountTypes.push({ value: '1', text: "Senior Citizen" });
    this.discountTypes.push({ value: '2', text: "PWD" });
  }

  validate() {
    this._spinner.show();

    var model =
    {
      cardNumber: this.cardNumber.value
    }
    this._customerService.validateCardForDiscount(model)
      .subscribe(
        result => {
          this._spinner.hide();
          this.card = result;
          this.balance = result.amount;
        },
        error => {
          this.card = null;
          this._spinner.hide();
          this.showSnackbarMessage(error.json().message);
        }
      );
  }

  onDiscountChanged()
  {
    if(this.discountType.value=="1")
    {
      this.discountIdName = "Senior Citizen Control No.";
      this.max = 10;
      this.idNumber.setValue("");
    }
    else if(this.discountType.value=="2")
    {
      this.discountIdName = "PWD ID";
      this.max = 12;
      this.idNumber.setValue("");
    }
  }

  back() {
    this._router.navigate([""]);
  }

  contiue() {
    this._spinner.show();

    var model =
    {
      cardNumber: this.cardNumber.value,
      discountType: this.discountType.value,
      idNumber: this.idNumber.value
    }
    this._customerService.registerCardToDiscountType(model)
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

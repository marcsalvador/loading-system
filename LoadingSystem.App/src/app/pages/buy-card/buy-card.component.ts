import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base';

@Component({
  selector: 'app-buy-card',
  templateUrl: './buy-card.component.html',
  styleUrls: ['./buy-card.component.css']
})
export class BuyCardComponent extends BaseComponent implements OnInit {

  expiryDate: Date = new Date();

  ngOnInit(): void {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth();
    var day = d.getDate();
    this.expiryDate = new Date(year + 5, month, day+1);
  }

  back()
  {
    this._router.navigate([""]);
  }

  contiue()
  {
    this._spinner.show();
    this._customerService.buyCard()
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

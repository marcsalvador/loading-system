import { Component, OnInit } from '@angular/core';
import { Card } from 'src/app/api/model';
import { BaseComponent } from '../base';

@Component({
  selector: 'app-successful',
  templateUrl: './successful.component.html',
  styleUrls: ['./successful.component.css']
})
export class SuccessfulComponent extends BaseComponent implements OnInit {

  public card: Card;

  ngOnInit(): void {
    this.card = JSON.parse(localStorage.getItem('qless-card'));
  }

  back(): void {
    this._router.navigate(['']);
  }
}

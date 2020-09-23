import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(private _router: Router) { }

  ngOnInit(): void {

  }

  buyCard()
  {
    this._router.navigate(["buy-card"]);
  }

  loadCard()
  {    
    this._router.navigate(["load-card"]);
  }

  discountRegistration()
  {
    this._router.navigate(["discount-registration"]);
  }

  useCard()
  {
    this._router.navigate(["use-card"]);    
  }
}

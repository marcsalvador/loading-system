import { Component } from "@angular/core";
import { Subject } from "rxjs";
import { NgxSpinnerService } from "ngx-spinner";
import { DatePipe } from "@angular/common";
import { Title } from "@angular/platform-browser";
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CustomerService } from '../api/customer.service';

@Component({
    template: ""
})
export class BaseComponent {

    constructor(
        public _router: Router,
        public _customerService: CustomerService,    
        public _spinner: NgxSpinnerService,
        public _datePipe: DatePipe,
        public _titleService: Title,
        public _snackbar: MatSnackBar,
    ) {

    }    

    showSnackbarMessage(msg: string, duration: number = 3000) {
        this._snackbar.open(msg, null, {
            duration: 5000,
        });
    }

    IsNotEmpty(str: any): boolean {
        if (str != null &&
            str != undefined &&
            str != "") {
            return true;
        }
        return false;
    }

    IsEmpty(str: any): boolean {
        if (str == null ||
            str == undefined ||
            str == "") {
            return true;
        }
        return false;
    }

}
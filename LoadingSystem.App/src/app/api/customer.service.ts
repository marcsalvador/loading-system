import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs";
import { map } from 'rxjs/operators';
import { Card } from './model';

@Injectable()
export class CustomerService {

    baseUrl: string = "http://localhost/qless/api";

    constructor(private _http: Http) {
    }

    buyCard(): Observable<Card> {
        return this._http.post(`${this.baseUrl}/customer/buy-card`, null)
            .pipe(map(response => response.json()));
    }

    loadCard(obj): Observable<Card> {
        return this._http.post(`${this.baseUrl}/customer/load-card`, obj)
            .pipe(map(response => response.json()));
    }

    validateCard(obj): Observable<Card> {
        return this._http.post(`${this.baseUrl}/customer/validate-card`, obj)
            .pipe(map(response => response.json()));
    }

    validateCardForDiscount(obj): Observable<Card> {
        return this._http.post(`${this.baseUrl}/customer/validate-card-discount`, obj)
            .pipe(map(response => response.json()));
    }

    registerCardToDiscountType(obj): Observable<Card> {
        return this._http.post(`${this.baseUrl}/customer/register-card`, obj)
            .pipe(map(response => response.json()));
    }

    getStationByLine(obj): Observable<any[]> {
        return this._http.post(`${this.baseUrl}/customer/get-station-by-line`, obj)
            .pipe(map(response => response.json()));
    }
    
    getStationPriceByOriginAndDestination(obj): Observable<any> {
        return this._http.post(`${this.baseUrl}/customer/get-station-fare`, obj)
            .pipe(map(response => response.json()));
    }

    useCard(obj): Observable<any> {
        return this._http.post(`${this.baseUrl}/customer/use-card`, obj)
            .pipe(map(response => response.json()));
    }
}

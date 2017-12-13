import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs/Rx';

@Component({
    template: `
    <h3>this page is not secured!</h3>

    <p><button (click)="callApi()">API</button></p>
`
})
export class UnsecuredComponent implements OnInit {
    constructor(private http1: HttpClient) { }

    ngOnInit() { }

    callApi() {
        this.http1.get('http://127.0.0.1:5001/identity')
        .toPromise()
        .then(resp => console.log('resp'))
        .catch(err => console.log(err));
    }
}

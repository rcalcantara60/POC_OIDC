import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Http } from '@angular/http';

import { AuthService } from 'app/common/services/auth.service';
import { User } from 'oidc-client';
import { Subscription } from 'rxjs';
import { tokenKey } from '@angular/core/src/view/util';
// import { HttpClient } from '@angular/common/http/src/client';

@Component({
    template: `
        <h3>this page is secured!</h3>

        <pre>{{ user | json }}</pre>

        <p><button (click)="callApi()">API</button></p>
    `
})
export class SecuredComponent implements OnInit, OnDestroy {
    userSub: Subscription;
    user: User;

    constructor(private authService: AuthService, private http1: HttpClient) {
        console.log(this.authService.getUser());
        this.userSub = this.authService.userLoadededEvent.subscribe(u => this.user = u);
    }

    ngOnInit() { }

    ngOnDestroy() {
        this.userSub.unsubscribe();
    }

    async callApi() {
        let tokenUser = await this.authService.mgr.getUser();
        const h = new HttpHeaders();
        h.set('Authorization', 'Bearer ' + tokenUser.access_token);
        this.http1.get('http://127.0.0.1:5001/identity', {
            headers: new HttpHeaders().set('Authorization', 'Bearer ' + tokenUser.access_token)
        }).subscribe();

    }
}

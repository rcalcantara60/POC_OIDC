import { Routes, RouterModule } from '@angular/router';
import { AuthGuardService } from 'app/common/guards/auth.guard';
import { AuthService } from 'app/common/services/auth.service';
import { SecuredComponent } from './secured/secured.component';
import { UnsecuredComponent } from 'app/unsecured/unsecured.component';

export const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/secured',
        pathMatch: 'full', canActivate: [AuthGuardService]
    },
    {
        path: 'secured',
        component: SecuredComponent, canActivate: [AuthGuardService]
    },
    {
        path: 'unsecured',
        component: UnsecuredComponent
    }
];
export const authProviders = [
    AuthGuardService,
    AuthService
];

import { Routes } from '@angular/router';
import {HomePageComponent} from "./pages/home-page/home-page.component";
import {LoginComponent} from "./components/auth/login/login.component";
import {AuthGuard} from "./guards/auth.guard";
import {RegisterComponent} from "./components/auth/register/register.component";
import {ProfilePageComponent} from "./pages/profile-page/profile-page.component";
import {StatisticsPageComponent} from "./pages/statistics-page/statistics-page.component";

export const routes: Routes = [
  {path:'home', component: HomePageComponent, canActivate: [AuthGuard]},
  {path: '', component: HomePageComponent, canActivate: [AuthGuard]},
  {path: 'login', component:LoginComponent},
  {path: 'register',component:RegisterComponent},
  {path: 'profile',component:ProfilePageComponent,canActivate: [AuthGuard]},
  {path : 'statistics',component: StatisticsPageComponent, canActivate : [AuthGuard]}
];

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AdminSigninComponent } from './admin/admin-signin/admin-signin.component'
import { AdminDashboardComponent } from './admin/admin-dashboard/admin-dashboard.component';
import { AdminPropertiesComponent } from './admin/admin-properties/admin-properties.component';
import { HomeComponent } from './home/home.component';
import { SinglePropertyComponent } from './single-property/single-property.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  { path: 'home',             component: HomeComponent },
  { path: 'property/:id',     component: SinglePropertyComponent },
  { path: 'admin/login',      component: AdminSigninComponent },
  { path: 'admin/dashboard',  component: AdminDashboardComponent, canActivate: [AuthGuardService] },
  { path: 'admin/properties', component: AdminPropertiesComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

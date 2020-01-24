import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  template: `
  <app-header></app-header>
  <main>
    <div id='main-content' class='container'>
     <!-- <div [@routeAnimations]="o && o.activatedRouteData && o.activatedRouteData['animation']"> -->
        <router-outlet #o="outlet" ></router-outlet>
      </div>

  </main>
  <app-footer></app-footer>
`,
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'pet-store';
}

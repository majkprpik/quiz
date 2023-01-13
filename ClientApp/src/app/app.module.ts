import { GameScreenComponent } from './game-screen/game-screen.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './login/login-page/login-page.component';

const routes: Routes = [
  {path: '', component: LoginPageComponent},
  {path: 'quiz', component: GameScreenComponent}
]

@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    GameScreenComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

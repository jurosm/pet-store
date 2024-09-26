import { ComponentFixture, TestBed } from '@angular/core/testing'
import { provideHttpClientTesting } from '@angular/common/http/testing'

import { LoginComponent } from './login.component'
import { provideHttpClient } from '@angular/common/http'

describe('LoginComponent', () => {
  let component: LoginComponent
  let fixture: ComponentFixture<LoginComponent>
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
      declarations: [LoginComponent],
    })

    fixture = TestBed.createComponent(LoginComponent)
    component = fixture.debugElement.componentInstance
  })
  it('it should create the LoginComponent', () => {
    expect(component).toBeTruthy()
  })
  afterAll(() => {
    TestBed.resetTestingModule()
  })
})

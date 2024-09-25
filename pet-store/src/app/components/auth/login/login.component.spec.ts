import { TestBed } from '@angular/core/testing'
import { LoginComponent } from './login.component'

describe('LoginComponent', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoginComponent],
    }).compileComponents()
  })
  it('it should create the LoginComponent', () => {
    const fixture = TestBed.createComponent(LoginComponent)
    const app = fixture.debugElement.componentInstance()
    expect(app).toBeTruthy()
  })
  afterAll(() => {
    TestBed.resetTestingModule()
  })
  afterEach(() => {
    TestBed.resetTestingModule()
  })
})

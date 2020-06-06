import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable()
export class GameOfLifeService {
    constructor( private http: Http,
        @Inject('BASE_URL') private baseUrl: string) {
    }

    startGame() {
        return this.http.get(this.baseUrl + 'api/GameOfLife/Start')
        .pipe(
          map(res => res.json())
        );
    }

    stopGame() {
        return this.http.get(this.baseUrl + 'api/GameOfLife/Stop')
        .pipe(
          map(res => res.json())
        );
    }
}

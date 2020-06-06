import { Component, NgZone, OnInit } from '@angular/core';
import { Row, Column, Cell, CellState } from './board.interface';

import { SignalRService } from '../services/signalr.service';
import { GameOfLifeService } from '../services/game.service';


@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.css']
})
export class BoardComponent implements OnInit {
    private rows: Row [] = [];

    constructor(
        private _gameService: GameOfLifeService,
        private _signalRService: SignalRService,
        private _ngZone: NgZone
      ) {}

    ngOnInit() {
        this.subscribeToEvents();
    }

    private subscribeToEvents(): void {
      this._signalRService._board.subscribe((board: any) => {
        this._ngZone.run(() => {
          const object = JSON.parse(board);
          this.mapData(object);
        });
      });
    }

    mapData(board: any) {
      const _rows = [] as Row [];
      board.forEach(function (row) {
        const _row = <Row>{};
        const columns = [] as Column[];
        const column = <Column>{};
        const cells = [] as Cell[];
        row.forEach(function (col) {
          const cell = col as Cell;
          cells.push(cell);
        });
        column.cells = cells;
        columns.push(column);
        _row.columns = columns;
        _rows.push(_row);
      });
      this.rows = _rows;
    }

    start() {
      this._gameService.startGame().subscribe(
        (data) => {
          console.log(data);
        },
        (error) => {
          console.log(error);
        });
    }

    stop() {
      this._gameService.stopGame().subscribe(
        (data) => {
          console.log(data);
        },
        (error) => {
          console.log(error);
        });
    }
}

export interface Row {
    columns: Column [];
}

export interface Column {
    cells: Cell [];
}

export interface Cell {
    row?: number;
    col?: number;
    cellStateName: string;
    cellState?: CellState;
}

export enum CellState {
    Dead,
    Live
}

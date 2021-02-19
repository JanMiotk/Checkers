import { Pawn } from './Pawn.js';
import { Queen } from './Queen.js';
import { Field } from './Field.js';

export class Board {
    fields = [];
    canvas;

    constructor(canvas) {
        this.SetFields();
        this.canvas = canvas.getContext('2d');
    }

    SetFields = () => {
        for (let y = 0; y < 8; y++) {
            for (let x = 0; x < 8; x++) {
                if ((y + x + y * 8) % 2 === 0) {

                    this.fields.push(new Field(x, y, x * 50 + 25, y * 50 + 25, 'Light', x * 50, y * 50, y + x + y * 8));
                }
                else {
                    if (y + x + y * 8 <= 25) {

                        this.fields.push(new Field(x, y, x * 50 + 25, y * 50 + 25, 'Dark', x * 50, y * 50, y + x + y * 8, new Pawn('Black', 'Pawn')));

                    }
                    else if (y + x + y * 8 >= 45) {
                        this.fields.push(new Field(x, y, x * 50 + 25, y * 50 + 25, 'Dark', x * 50, y * 50, y + x + y * 8, new Pawn('White', 'Pawn')));
                    }
                    else {
                        this.fields.push(new Field(x, y, x * 50 + 25, y * 50 + 25, 'Dark', x * 50, y * 50, y + x + y * 8));
                    }
                }
            }
        }
    };

    DrawBoard = () => {
        let pole = 0;
        let licznik = 0;

        for (let i = 0; i < 64; i++) {
            if (this.fields[i].colour == "Light") {
                this.canvas.fillStyle = 'rgb(197, 161, 77)'
                this.canvas.fillRect(this.fields[i].begin_x, this.fields[i].begin_y, 50, 50)
            }
            else {
                this.canvas.fillStyle = 'rgb(97, 53, 28)'
                this.canvas.fillRect(this.fields[i].begin_x, this.fields[i].begin_y, 50, 50)
            }
        }
    };

    DrawPawns = () => {
        for (let i = 0; i < 64; i++) {
            if (this.fields[i].pawn != null) {
                this.DrawPawn(i);
            }
        }
    };

    DrawPawn = (index) => {
        this.canvas.beginPath();
        this.canvas.arc(this.fields[index].center_x, this.fields[index].center_y, 20, 0, 2 * Math.PI)

        if (this.fields[index].pawn.name == "Pawn") {

            if (this.fields[index].pawn.colourName == "White") {
                this.canvas.fillStyle = 'rgb(255,255,255)';
            }
            else {
                this.canvas.fillStyle = 'rgb(0,0,0)';
            }
        }

        if (this.fields[index].pawn.name == "Queen") {
            if (this.fields[index].pawn.colourName == "White") {
                this.canvas.fillStyle = 'rgb(212,175,55)';
            }
            else {
                this.canvas.fillStyle = 'rgb(192,192,192)';
            }
        }

        this.canvas.fill();
        this.canvas.closePath();
    }

    ReturnIndexOnBoard = (canvas, event) => {
        let rect = canvas.getBoundingClientRect();
        let x = Math.floor((event.clientX - rect.left) / 50);
        let y = Math.floor((event.clientY - rect.top) / 50);
        return x + y * 8
    }

    SelectPawn = (index, prevIndex, colour) => {

        if (this.fields[index].pawn == null) {

            return prevIndex;
        }
        else if (this.fields[index].pawn.colourName != colour) {

            return prevIndex;
        }
        else {

            if (this.fields[index].pawn == "Queen") {
                let temp = 12;
            }

            this.UnmarkPawn();
            this.canvas.beginPath();
            this.canvas.arc(this.fields[index].center_x, this.fields[index].center_y, 20, 0, 2 * Math.PI);

            if (this.fields[index].pawn.name == "Pawn") {
                this.canvas.fillStyle = 'rgb(0,255,0)';
            }
            else {
                this.canvas.fillStyle = 'rgb(13, 161, 229)';
            }

            this.canvas.fill();
            this.canvas.closePath();
            this.fields[index].isMarked = true;
            return index;
        }
    }

    UnmarkPawn = () => {
        for (let i = 0; i < 64; i++) {
            if (this.fields[i].pawn != null && this.fields[i].isMarked) {
                this.DrawPawn(i);
                this.fields[i].isMarked = false;
                break;
            }
        }
    }

    Move = (pawnIndex, fieldIndex) => {

        let behavior = this.fields[pawnIndex].pawn.Move(pawnIndex, fieldIndex, this);
        if (behavior != -2) {

            this.fields[fieldIndex].pawn = null;
            this.fields[fieldIndex].pawn = this.fields[pawnIndex].pawn;
            this.fields[pawnIndex].pawn = null;

            if (behavior != 0) {
                this.fields[behavior].pawn = null;
            }
            this.ChangeToQueen(fieldIndex, pawnIndex);
            this.DrawBoard();
            this.DrawPawns();
            return true;
        }
        else {
            return false;
        }
    }

    ChangeToQueen = (index) => {
        if ((this.fields[index].y == 0 && this.fields[index].pawn.colourName == "White") ||
            (this.fields[index].y == 7 && this.fields[index].pawn.colourName == "Black")) {
            this.fields[index].pawn = new Queen(this.fields[index].pawn.colourName, "Queen");


        }
    }
}
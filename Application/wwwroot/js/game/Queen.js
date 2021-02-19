import { Model } from './Model.js';

export class Queen extends Model {

    constructor(colourName, name) {
        super(colourName, name);
    }

    CalculateMoves = (pawnIndex, board) => {
        let multipiler = 0;
        let directions = [-9, -7, 7, 9];
        let Schema = {
            movements: [],
            capturing: []
        };
        for (let i = 0; i < 28; i++) {
            let index = i % 4;
            if (i % 4 == 0) {
                multipiler++;
            }
            if (directions[index] == 0) {
                continue;
            }
            let value = pawnIndex + directions[index] * multipiler;
            if (value <= 63 && value >= 0 && board.fields[value].pawn == null && board.fields[value].index % 2 == 1) {
                let tempMultipiler = multipiler - 1;
                let oneFieldBefore = pawnIndex + directions[index] * tempMultipiler;
                let twoFieldsBefore = pawnIndex + directions[index] * --tempMultipiler;

                if ((board.fields[oneFieldBefore] != undefined && board.fields[oneFieldBefore].pawn != null) &&
                    (board.fields[twoFieldsBefore] != undefined && board.fields[twoFieldsBefore].pawn != null) &&
                    (pawnIndex != oneFieldBefore) &&
                    (pawnIndex != twoFieldsBefore)) {
                    directions[index] = 0;
                    continue;
                }

                if ((board.fields[oneFieldBefore].pawn != null) &&
                    (pawnIndex != oneFieldBefore)) {
                    directions[index] = 0;
                }
                if ((board.fields[oneFieldBefore].pawn != null) &&
                    (board.fields[oneFieldBefore].pawn.colourName == board.fields[pawnIndex].pawn.colourName) &&
                    (pawnIndex != oneFieldBefore)) {
                    continue;
                }
                Schema.movements.push(value);
                if (multipiler > 1 && board.fields[oneFieldBefore].pawn != null) {
                    Schema.capturing.push(oneFieldBefore);
                } else {
                    Schema.capturing.push(0);
                }
            }
        }
        return Schema;
    }
}
import { Model } from './model.js';

export class Pawn extends Model {


    constructor(colourName, name) {
        super(colourName, name);
    };
    CalculateMoves = (pawnIndex, board) => {
        let multipiler = 0;
        let directions = [-9, -7, 7, 9];
        let Schema = {
            movements: [],
            capturing: []
        };

        for (let i = 0; i < 8; i++) {
            let index = i % 4;
            if (i % 4 == 0) {
                multipiler++;
            }
            let value = pawnIndex + directions[index] * multipiler;
            if (value <= 63 && value >= 0 && board.fields[value].pawn == null) {
                let tempMultipiler = multipiler - 1;
                let oneFieldBefore = pawnIndex + directions[index] * tempMultipiler;
                if ((board.fields[oneFieldBefore].pawn != null &&
                    board.fields[oneFieldBefore].pawn.colourName != board.fields[pawnIndex].pawn.colourName &&
                    pawnIndex != oneFieldBefore && multipiler > 1) ||
                    (multipiler == 1)) {

                    if ((board.fields[pawnIndex].pawn.colourName == "White" && multipiler == 1 && directions[index] > 0) ||
                        (board.fields[pawnIndex].pawn.colourName == "Black" && multipiler == 1 && directions[index] < 0)) {
                        continue;
                    }
                    Schema.movements.push(value);
                    if (multipiler > 1) {
                        Schema.capturing.push(oneFieldBefore);
                    } else {
                        Schema.capturing.push(0);
                    }
                }
            }
        }
        return Schema;
    }
}





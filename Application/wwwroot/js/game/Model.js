export class Model {
    name;
    colourName;

    constructor(colourName, name) {
        this.colourName = colourName;
        this.name = name;

    };

    CalculateMoves = () => { };

    Move = (pawnIndex, fieldIndex, board) => {
        var Schema = this.CalculateMoves(pawnIndex, board);
        return this.ReturnBehavior(fieldIndex, Schema);
    }

    ReturnBehavior = (fieldIndex, Schema) => {
        let behavior = -2;
        for (let i = 0; i < Schema.movements.length; i++) {
            let index = Schema.movements[i];
            let beatenIndex = Schema.capturing[i];
            if (index == fieldIndex) {
                if (beatenIndex > 0) {
                    behavior = beatenIndex
                }
                else {
                    behavior = 0;
                }
                break;
            }
        }
        return behavior
    }
}
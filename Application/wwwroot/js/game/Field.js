export class Field {
    x;
    y;
    begin_x;
    begin_y;
    center_x;
    center_y;
    colour;
    index;
    pawn;
    isMarked;
    constructor(x, y, center_x, center_y, colour, begin_x, begin_y, index, pawn = null, isMarked = false) {
        this.x = x;
        this.y = y;
        this.center_x = center_x;
        this.center_y = center_y;
        this.colour = colour;
        this.index = index;
        this.begin_x = begin_x;
        this.begin_y = begin_y;
        this.pawn = pawn;
        this.isMarked = isMarked;
    }
}
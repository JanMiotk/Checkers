import { Board } from "./board.js"

var canvas = document.getElementById("Board");
var communicator = document.querySelector("textarea");
var poz1 = document.querySelector(".possition1-btn");
var poz2 = document.querySelector(".possition2-btn");
var scoreBox = document.querySelector(".score-box");
var closeInfo = document.querySelector(".close-info");
var backLink = document.querySelector("#back-link");
var text = document.getElementById("text");
var messageButton = document.getElementById("messageButton");
var socket = new WebSocket(`wss://localhost:44354/Game/Room?name=${room}`);

var message = {}

var players = {

}

var whoseTurn = 'White';
var board = new Board(canvas);


board.DrawBoard();
board.DrawPawns();

var prevIndex = -1;
canvas.addEventListener('click', function (e) {

    if ((poz1.value == nick || poz2.value == nick) && whoseTurn == players[nick] && poz1.value != '1#' && poz2.value != '2#') {
        let index = board.ReturnIndexOnBoard(canvas, e)
        prevIndex = board.SelectPawn(index, prevIndex, players[nick]);
        if (prevIndex != index) {

            if (prevIndex != -1 && board.fields[index].pawn == null) {
                
                let isMoved = board.Move(prevIndex, index);
                if (isMoved == true) {

                    whoseTurn = SetWhoseTurn(players[nick]);
                    message.type = "Board";
                    message.index = index;
                    message.prevIndex = prevIndex;
                    message.whoseTurn = whoseTurn;
                    message.userName = nick;
                    message.colour = players[nick];
                    var result = CountPawns();
                    message.white = result[0];
                    message.black = result[1];
                    prevIndex = -1
                    socket.send(JSON.stringify(message));
                }
            }
        }
    }
});

poz1.addEventListener('click', (e) => {

    if (poz1.value != nick && poz2.value != nick && poz1.value == '1#') {
        message.type = "Position1";
        message.userName = nick;
        message.position = "Position1";
        players[nick] = 'White'
        socket.send(JSON.stringify(message));
    }
});

poz2.addEventListener('click', (e) => {

    if (poz1.value != nick && poz2.value != nick && poz2.value == '2#') {
        message.type = "Position2";
        message.userName = nick;
        message.position = "Position2";
        players[nick] = 'Black'
        socket.send(JSON.stringify(message));
    }
});

backLink.addEventListener('click', (e) => { 
    message.type = "Leave";
    message.userName = nick;
    message.colour = players[nick];
    message.position = players[nick] == "White" ? "Position1" : "Position2";
    socket.send(JSON.stringify(message));
    window.location.href = backLink.getAttribute('href');
});

messageButton.addEventListener('click', (e) => {
    if (text.value != null) {
        message.type = "Message";
        message.message = text.value;
        message.userName = nick
        text.value = "";
        socket.send(JSON.stringify(message));
    }
})

closeInfo.addEventListener('click', (e) => {
    canvas.classList.remove('hide');
    scoreBox.classList.remove('show');
}) 

socket.onopen = function (event) {
    message.type = "Join";
    message.userName = nick;
    message.connection = '';
    communicator.disabled = true;
    socket.send(JSON.stringify(message));
};

socket.onmessage = (event) => {
    var msg = JSON.parse(event.data)

    switch (msg.type) {
        case "Join":
            communicator.value += `Dołączył: ${msg.userName} \n`;
            if (msg.userName == nick) {
                backLink.setAttribute('href', `Room?name=${room}&connection=${msg.connection}`);
            }

            break;
        case "Position1":
            BlockSit(msg.position, msg.userName);
            break;
        case "Position2":
            BlockSit(msg.position, msg.userName);
            break;
        case "Message":
            communicator.value += `${msg.userName}: ${msg.message} \n`
            break;
        case "Board":
            if (msg.userName != nick) {
                board.Move(msg.prevIndex, msg.index)
                whoseTurn = msg.whoseTurn;
            }

            if (msg.white == 0 || msg.black == 0) {
                canvas.classList.add('hide');
                scoreBox.classList.add('show');
                var pawns = msg.colour == 'White' ? msg.white : msg.black
                scoreBox.children[1].innerText = 'Wygrał ' + msg.userName;
                scoreBox.children[2].innerText = 'Kolor ' + msg.colour;
                scoreBox.children[3].innerText = 'Pozostało pionków  ' + pawns;
            }
            break;
        case "Leave":
            UnlockSit(msg.position)
            communicator.value += `Opuścił: ${msg.userName}\n`
            board = new Board(canvas)
            board.DrawBoard();
            board.DrawPawns();
            whoseTurn = 'White';
            break;
    }
}

function BlockSit(position,name) {

    if (position == "Position1") {
        poz1.value = name;
        poz1.disabled = true;
    }

    if (position == "Position2") {
        poz2.value = name;
        poz2.disabled = true;
    }
}

function UnlockSit(position) {

    if (position == "Position1") {
        poz1.disabled = false;
        poz1.value = "1#";
    }

    if (position == "Position2") {
        poz2.disabled = false;
        poz2.value = "2#";
    }
}

function SetWhoseTurn(colour) {
    if (colour == 'White')
        return 'Black';

    return 'White';
}

function CountPawns() {
    var white = 0;
    var black = 0;
    board.fields.forEach((el) => {
        
        if (el.pawn != null) {

            if (el.pawn.colourName == 'White') {
                white++;
            }

            if (el.pawn.colourName == 'Black') {
                black++;
            }
        }
    })
    return [white, black]
}

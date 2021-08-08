const vid = document.getElementById("container");
document.addEventListener('keypress', printKey);
requestAnimationFrame(clockUpdate);

function printKey(e) {
    var time = getTime();
    var ts = time[0] + ':' + time[1] + ':' + time[2] + ':' + time[3];

    const table = document.getElementById("time");
    let row = table.insertRow(1);
    var color = colourKey(e.code);
    document.getElementById('clock').style.backgroundColor = color;
    row.insertCell(0).innerHTML = e.code;
    row.insertCell(1).innerHTML = ts;
    row.insertCell(2).innerHTML = "<p style='background-color:" + color + ";'>" + e.code + "</p>";
}

function colourKey(k) {
    if (k === 'KeyW'){
        return "green";
    } else if (k === 'KeyA'){
        return "red";
    } else if (k === 'KeyS'){
        return "yellow";
    } else if (k === 'KeyD'){
        return "blue";
    } else {
        return "white";
    }
}

function getTime(){
    const date = new Date();

    var h = addZero(date.getHours());
    var m = addZero(date.getMinutes());
    var s = addZero(date.getSeconds());
    var ms = addZero(date.getMilliseconds(), true);

    return [h, m, s, ms];
}

function clockUpdate(){
    let time = getTime();
    let cl = document.getElementsByClassName('digital-clock');
    cl[0].innerText = time[0] + ':' + time[1] + ':' + time[2] + ':' + time[3];
    requestAnimationFrame(clockUpdate);
}

function addZero(x, isMs=false) {
    if (isMs) {
        if (x > 0 && x < 10) {
            return x = '00' + x;
        } else if (x >= 10 && x < 100){
            return x = '0' + x;
        } else {
            return x;
        }
    } else {
        if (x < 10) {
            return x = '0' + x;
        } else {
            return x;
        }
    }
}
﻿function oneVsoneQueue(SummonerId, Region) {

    $.ajax({
        type: 'POST',
        url: 'http://matchmakingapi.azurewebsites.net/oneVone/JoinQueue?summonerID=' + SummonerId,
        success: function (msg) {
            $(".mm_gametype").css({ "display": "none" });
            $(".searching_wrap").toggle();
            CheckIfMatchFound(SummonerId);
            startTimer();
        }      
    });
}

function startTimer() {
    var elapsed_seconds = 0;
    setInterval(function () {
        elapsed_seconds = elapsed_seconds + 1;
        $(".searching_circle>p").text(get_elapsed_time_string(elapsed_seconds));
    }, 1000);
}

function get_elapsed_time_string(total_seconds) {
    function pretty_time_string(num) {
        return (num < 10 ? "0" : "") + num;
    }

    var hours = Math.floor(total_seconds / 3600);
    total_seconds = total_seconds % 3600;

    var minutes = Math.floor(total_seconds / 60);
    total_seconds = total_seconds % 60;

    var seconds = Math.floor(total_seconds);

    // Pad the minutes and seconds with leading zeros, if required
    hours = pretty_time_string(hours);
    minutes = pretty_time_string(minutes);
    seconds = pretty_time_string(seconds);

    // Compose the string for display
    var currentTimeString =  minutes + ":" + seconds;

    return currentTimeString;
}

function CheckIfMatchFound(SummonerId) {
    var interval = setInterval(function () {
        $.ajax({
            type: 'POST',
            url: 'http://matchmakingapi.azurewebsites.net/oneVone/MatchFound?SummonerId=' + SummonerId,
            success: function (msg) {
                if (msg !== 0) {
                    clearInterval(interval);
                    MatchFoundAccept(msg, SummonerId);
                }
            }
        });
    }, 2000);
}

function MatchFoundAccept(MatchId, SummonerId) {
    $.ajax({
        type: 'POST',
        url: 'http://matchmakingapi.azurewebsites.net/oneVone/AcceptQueue?summonerId=' + SummonerId + '&MatchId=' + MatchId,
        success: function (msg) {
            JoinMatch(MatchId, SummonerId);
        }
    });
}

function JoinMatch(MatchId, SummonerId) {
    $.ajax({
        type: 'POST',
        url: 'Matchmaking/CreateGame',
        data: { summonerId: SummonerId, match: MatchId },
        success: function (msg) {
            
        }
    });
}

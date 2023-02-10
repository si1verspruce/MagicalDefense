mergeInto(LibraryManager.library, {

  GetReviewAvailability: function () {
    ysdk.feedback.canReview()
      .then(({ value, reason }) => {
        if (value) {
          gameInstance.SendMessage('ReviewGame', 'YandexSetReviewAvailability', Number(value));
        } else {
          console.log(reason)
        }
      })
  },

  OpenReviewWindow: function () {
    ysdk.feedback.canReview()
      .then(({ value, reason }) => {
        if (value) {
          ysdk.feedback.requestReview()
            .then(({ feedbackSent }) => {
              console.log(feedbackSent);
            })
        } else {
          console.log(reason)
        }
      })
  },

  SaveExtern: function(date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
  },

  LoadExtern: function() {
    player.getData().then(_date => {
      const myJSON = JSON.stringify(_date);
      gameInstance.SendMessage('SaveLoad', 'LoadExternData', myJSON);
    });
  },

  SetToLeaderboard: function(value) {
    ysdk.getLeaderboards()
    .then(lb => {
      lb.setLeaderboardScore('StageNumber', value);
    });
  },

  GetLanguageCode: function() {
    var language = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(language) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(language, buffer, bufferSize);
    
    return buffer;
  },

  ShowInterstitial: function() {
    ysdk.adv.showFullscreenAdv({ })
  }

});
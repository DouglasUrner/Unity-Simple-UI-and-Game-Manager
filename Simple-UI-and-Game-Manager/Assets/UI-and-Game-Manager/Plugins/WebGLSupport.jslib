mergeInto(LibraryManager.library, {
  // Quit WebGL player by loading a new page, passed as 'url',
  // for example to go back to a menu of games. The value 'url'
  // is set when GameInfo.json is loaded in UIManager.
  WebGLQuit: function (url) {
    // Possible useful for debugging, but it the alert will appear
    // multiple times before the URL opens. Don't be surprised...
    // window.alert("WebGLQuit() loading: " + UTF8ToString(url));
    window.open(UTF8ToString(url), '_self');
  },
});
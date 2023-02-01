mergeInto(LibraryManager.library, {
  WebGLQuit: function (url) {
    window.alert("WebGLQuit()");
    window.open(url, '_self');
  },
});
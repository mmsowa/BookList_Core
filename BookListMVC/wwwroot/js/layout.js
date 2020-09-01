const themeCheckBox = document.querySelector('input[name=theme]');
let themeCheckboxValue = JSON.parse(localStorage.getItem('themeCheckboxValue')) || {};

$(document).ready(function () {
  $.each(themeCheckboxValue, function (key, value) {
    $("#" + key).prop('checked', value);
  });

  themeCheckBox.addEventListener('change', function () {
    trans()
    localStorage.setItem("themeCheckboxValue", JSON.stringify(this.checked));
    if (this.checked) {
      document.documentElement.setAttribute('data-theme', 'dark')
    } else {
      document.documentElement.setAttribute('data-theme', 'light')
    }
  })
});

function trans() {
  document.documentElement.classList.add('transition');
  window.setTimeout(() => {
    document.documentElement.classList.remove('transition')
  }, 300)
}
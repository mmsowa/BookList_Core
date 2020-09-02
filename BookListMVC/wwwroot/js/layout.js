const themeCheckBox = document.querySelector('input[name=theme]');
const themeCheckboxValue = JSON.parse(localStorage.getItem('themeCheckboxValue')) || {};
const isChecked = JSON.parse(localStorage.getItem("themeCheckboxValue"));

document.getElementById("toggle-theme-checkbox").checked = isChecked;
if (themeCheckBox.checked) {
  document.documentElement.setAttribute('data-theme', 'dark')
} else {
  document.documentElement.setAttribute('data-theme', 'light')
}

$(document).ready(function () {
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
const themeCheckBox = document.querySelector('input[name=theme]');
const themeCheckboxValue = JSON.parse(localStorage.getItem('themeCheckboxValue')) || {};
const isChecked = JSON.parse(localStorage.getItem("themeCheckboxValue"));

document.getElementById("toggle-theme-checkbox").checked = isChecked;
toggleTheme();

$(document).ready(function () {
  themeCheckBox.addEventListener('change', function () {
    trans()
    localStorage.setItem("themeCheckboxValue", JSON.stringify(this.checked));
    toggleTheme();
  })
});

function trans() {
  document.documentElement.classList.add('transition');
  window.setTimeout(() => {
    document.documentElement.classList.remove('transition')
  }, 300)
}

function toggleTheme() {
  if (themeCheckBox.checked) {
    document.documentElement.setAttribute('data-theme', 'dark')
  } else {
    document.documentElement.setAttribute('data-theme', 'light')
  }
}

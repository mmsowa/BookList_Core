function getAppUserBooksForActiveUsern() {
  return $.get("/Debug/GetAppUserBooksForActiveUser", "", (data) => console.log(data));
}
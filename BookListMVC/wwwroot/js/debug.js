function testButton() {
  return $.get("/Debug/GetAppUserBooksForActiveUser", "", (data) => console.log(data));
}
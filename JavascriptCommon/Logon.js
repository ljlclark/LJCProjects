// Copyright Lester J.Clark and Contributors
// Licensed under the MIT license.
// LogOn.js

class LogOn
{
  AddEvents()
  {
    let eForm = document.forms["LogOnForm"];
    eForm.addEventListener("submit", this.Validate.bind(this));
  }

  Validate(event)
  {
    let retValue = true;

    let eForm = document.forms["LogOnForm"];
    let ePassword = eForm["Password"];
    let password = ePassword.value;
    if (password.length < 8)
    {
      alert("Password must be at least 8 characters.");
      //alert(event.target.tagName);
      event.preventDefault();
      ePassword.focus();
      retValue = false;
    }
    if (retValue)
    {
      localStorage.setItem("UserName", UserName.value);
      localStorage.setItem("Password", password);
      UserName.value = "";
      ePassword.value = "";
    }
    return retValue;
  }
}
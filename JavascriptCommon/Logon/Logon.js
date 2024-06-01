// Copyright Lester J.Clark and Contributors
// Licensed under the MIT license.
// LogOn.js

// The Logon class.
class LogOn
{
  // Add the LogOn events.
  AddEvents()
  {
    let eForm = document.forms["logonForm"];
    eForm.addEventListener("submit", this.Validate.bind(this));
  }

  // Validate the form values.
  Validate(event)
  {
    let retValue = true;

    //alert(event.target.tagName);
    message.innerText = "";
    let eForm = document.forms["logonForm"];
    let eAccessCode = eForm["AccessCode"];
    let accessCode = eAccessCode.value;
    if (accessCode.length < 8)
    {
      let text = "AccessCode must be at least 8 characters.";
      message.innerText = text;
      event.preventDefault();
      eAccessCode.focus();
      retValue = false;
    }
    if (retValue)
    {
      localStorage.setItem("UserName", UserName.value);
      localStorage.setItem("AccessCode", accessCode);
      UserName.value = "";
      eAccessCode.value = "";
    }
    return retValue;
  }
}
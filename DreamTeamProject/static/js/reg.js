
const errorTextBoxColor = "rgba(254, 100, 100, 0.8)";
const normalTextBoxColor = window.getComputedStyle(document.getElementById("first name")).backgroundColor;

function register() {

    let name = document.getElementById("first name");
    let surname = document.getElementById("last name");
    let email = document.getElementById("email");
    let phone = document.getElementById("tel");
    let password = document.getElementById("password");
    let confirm_password = document.getElementById("re-password");

    function checkElementAnEmpy(el) {

        if (el.value === "") {

            el.style.backgroundColor = errorTextBoxColor;
            return false;

        } else {
            
            el.style.backgroundColor = normalTextBoxColor;
            return true;
        }
    }

    let isOk = [name, surname, email, phone, password, confirm_password].every(checkElementAnEmpy);
    if (!isOk) return;

    if (password.value !== confirm_password.value) {

        password.style.backgroundColor = errorTextBoxColor;
        confirm_password.style.backgroundColor = errorTextBoxColor;
        return;

    }

    let data = {
        FirstName: name.value,
        SecondName: surname.value,
        Mail: email.value,
        Phone: phone.value,
        Password: password.value,
        ConfirmedPassword: confirm_password.value
    };
    fetch("/api/users",
        {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json"  
            },
            body: JSON.stringify(data)
        }
    );
    document.location.replace("/");
}

document.getElementById("register").addEventListener("click", function(e) {
    e.stopPropagation();
    register();
});
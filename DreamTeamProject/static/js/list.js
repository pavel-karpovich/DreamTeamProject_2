
let people = [];


// What the f*ck, guys...
/* <tr>
<td width="592" height="80" align="left" valign="middle"><li><t2><em>Александр Попов</em></t2></li></td>
</tr> */
function createDumbListItem(fullName) {

    let tr = document.createElement("tr");
    let td = document.createElement("td");
    td.setAttribute("width", "592");
    td.setAttribute("height", "80");
    td.setAttribute("align", "left");
    td.setAttribute("valign", "middle");
    tr.appendChild(td);
    let li = document.createElement("li");
    td.appendChild(li);
    let t2 = document.createElement("t2");
    li.appendChild(t2);
    let em = document.createElement("em");
    em.textContent = fullName;
    t2.appendChild(em);

    return tr;
}

let dumbList = document.querySelector("table.r");

function updateList() {

    fetch("/api/users", { method: "GET", cache: "no-cache" })
        .then((response) => response.json())
        .then((json) => {

            for (let user of json) {

                if (!people.includes(user)) {

                    let item = createDumbListItem(user.FirstName + " " + user.SecondName);
                    dumbList.appendChild(item);
                    people.push(user);

                }
            }
        });
}

updateList();

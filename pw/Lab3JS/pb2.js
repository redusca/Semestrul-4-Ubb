function validare(){
    let nume = document.getElementById("nume");
    let dataNasteri = document.getElementById("dataNasteri");
    let varsta = document.getElementById("varsta");
    let email = document.getElementById("email");
    var mesaj = document.getElementById('mesaj');

    nume.classList.remove('invalid');
    dataNasteri.classList.remove('invalid');
    varsta.classList.remove('invalid');
    email.classList.remove('invalid');
    mesaj.classList.remove('invalid');
    mesaj.textContent = '';

    let mesajValidare = '';
    let campurivalidare = [];

    if(!nume.value){
        nume.classList.add('invalid');
        mesajValidare += 'Numele este obligatoriu.\n';
        campurivalidare.push("nume");
    }

    if(!dataNasteri.value){
        dataNasteri.classList.add('invalid');
        mesajValidare += 'Data nasterii este obligatorie.\n';
        campurivalidare.push("dataNasteri");
    }

    if(!varsta.value){
        varsta.classList.add('invalid');
        mesajValidare += 'Varsta este obligatorie.\n';
        campurivalidare.push("varsta");
    }
    else if(!varsta.value){
        varsta.classList.add('invalid');
        mesajValidare += 'Varsta trebuie sa fie un numar intreg pozitiv.\n';
        campurivalidare.push("varsta");
    }

    if (!email.value){
        email.classList.add('invalid');
        mesajValidare += 'Emailul este obligatoriu.\n';
        campurivalidare.push("email");
    }
    else if(!email.value.includes('@')){
        email.classList.add('invalid');
        mesajValidare += 'Emailul trebuie sa contina @.\n';
        campurivalidare.push("email");
    }

    if(mesajValidare.length > 0){
        mesaj.classList.add('invalid');
        mesaj.textContent = `Campurile ${campurivalidare} nu sunt completate corect.\n ${mesajValidare}`;
        return
    }

    mesaj.textContent = 'Datele sunt completate corect.';
    
    return;
}
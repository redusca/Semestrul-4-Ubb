function validare(){
    let nume = $("#nume");
    let dataNasteri = $("#dataNasteri");
    let varsta = $("#varsta");
    let email = $("#email");
    let mesaj = $("#mesaj");

    nume.removeClass('invalid');
    dataNasteri.removeClass('invalid');
    varsta.removeClass('invalid');
    email.removeClass('invalid');
    mesaj.removeClass('invalid');
    mesaj.text(' ');

    let mesajValidare = '';
    let campurivalidare = [];

    if(!nume.val()){
        nume.addClass('invalid');
        mesajValidare += 'Numele este obligatoriu.\n';
        campurivalidare.push("nume");
    }

    if(!dataNasteri.val()){
        dataNasteri.addClass('invalid');
        mesajValidare += 'Data nasterii este obligatorie.\n';
        campurivalidare.push("dataNasteri");
    }

    if(!varsta.val()){
        varsta.addClass('invalid');
        mesajValidare += 'Varsta este obligatorie.\n';
        campurivalidare.push("varsta");
    }
    if(!$.isNumeric(varsta.val()) || varsta.val() < 0 || Math.trunc(varsta.val()) != varsta.val()){
        varsta.addClass('invalid');
        mesajValidare += 'Varsta trebuie sa fie un numar intreg pozitiv.\n';
        campurivalidare.push("varsta");
    }

    if (!email.val()){
        email.addClass('invalid');
        mesajValidare += 'Emailul este obligatoriu.\n';
        campurivalidare.push("email");
    }
    else if(!email.val().includes('@')){
        email.addClass('invalid');
        mesajValidare += 'Emailul trebuie sa contina @.\n';
        campurivalidare.push("email");
    }

    if(mesajValidare.length > 0){
        mesaj.addClass('invalid');
        mesaj.text(`Campurile ${campurivalidare} nu sunt completate corect.\n ${mesajValidare}`);
        return
    }

    mesaj.text('Datele sunt completate corect.');
    
    return;
}

$("input[type='button']").click(function(){
    validare();
})
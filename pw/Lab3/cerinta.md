# Cerințe

## 1. Mutare elemente între liste

Se va scrie o pagină HTML care conține două liste cu mai multe elemente fiecare, create cu ajutorul tagului `<select>`. La un dublu click pe un element al primei liste, acesta va fi mutat în lista a doua și invers. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

## 2. Formular web cu validare

Un formular web va permite unui utilizator să își introducă numele, data nașterii, vârsta și adresa de e-mail. La apăsarea unui buton “Trimite” se vor valida toate aceste câmpuri dacă sunt completate și dacă sunt completate corect. Dacă da, se va afișa un mesaj “Datele sunt completate corect”, altfel, se va afișa un mesaj de genul “Câmpurile nume și vârstă nu sunt completate corect”, aceste câmpuri fiind încercuite într-o bordură roșie. Toate aceste validări vor fi implementate pe client în JavaScript. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

## 3. Memory Game

Se va implementa folosind JavaScript următoarea problemă: o matrice cu număr par de elemente, reprezentată vizual sub forma unui tabel, conține perechi de numere inițial ascunse. Dacă utilizatorul dă click pe două celule ale tabelului ce conțin numere egale, acestea vor fi afișate și vor rămâne afișate. Dacă numerele conținute în cele două celule nu sunt egale, vor fi ascunse din nou după un număr de 2-3 secunde. Jocul se termină când toate perechile de numere au fost ghicite. 

După prima implementare a jocului, se va crea o nouă versiune în care numerele vor fi înlocuite cu imagini (ce conțin fructe, spre exemplu, sau “profi” de pe pagina facultății). Problema mai este recunoscută și sub numele de Memory Game. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

## 4. Tabel sortabil

Se va scrie o pagină HTML care conține un tabel cu cel puțin trei coloane și cel puțin trei linii. Prima coloană reprezintă antetul. Când utilizatorul va da click pe o celulă din antet, elementele din tabel se vor sorta crescător sau descrescător în funcție de valorile prezente pe linia corespunzătoare antetului pe care s-a dat click. Codul JavaScript va fi reutilizabil și va permite crearea unui comportament de tabel sortabil pentru mai multe tabele existente în cadrul paginii. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

Exemplu de tabel ce se dorește a fi sortat (sortarea se va face alfabetic după numele fructului, după preț sau după cantitate):

<table style="width: 180px;">
    <tbody>
        <tr>
            <th>Fructe</th>
            <td>Mere</td>
            <td>Pere</td>
        </tr>
        <tr>
            <th>Preț</th>
            <td>3</td>
            <td>4</td>
        </tr>
        <tr>
            <th>Cantitate</th>
            <td>6</td>
            <td>8</td>
        </tr>
    </tbody>
</table>

După rezolvarea problemei, se va implementa o nouă variantă în care capul de tabel este orizontal, nu vertical. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

## 5. Carousel cu listă ordonată

Într-o pagină HTML există o listă descrisă cu ajutorul tagului `<ol>`. Fiecare element din listă (`<li>`) conține o imagine (`<img>`) și un link (`<a>`). Elementele listei, mai puțin primul dintre ele, nu sunt vizibile inițial (se poate folosi în acest sens CSS). 

Afișarea unui element din listă presupune afișarea imaginii și a textului ca link peste imagine (a se vedea ca exemplu carouselul din pagina facultății). După `n` secunde, printr-un efect de tranziție, va fi afișat următorul element din listă. Se vor implementa și două butoane Next și Previous care vor permite afișarea elementelor următoare sau anterioare fără a se aștepta trecerea celor `n` secunde. Nu se vor folosi biblioteci de funcții, jQuery, pluginuri, etc.

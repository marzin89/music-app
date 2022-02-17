'use strict';

// Variabler
const firstPage = document.getElementsByClassName('first-page');
const secondPage = document.getElementsByClassName('second-page');
const nextLink = document.getElementById('next-page');
const previousLink = document.getElementById('previous-page');

// Händelsehanterare
nextLink.addEventListener('click', (e) => {
    e.preventDefault();
    toggle(e);
})
previousLink.addEventListener('click', (e) => {
    e.preventDefault();
    toggle(e);
})

// Funktion som "bläddrar" bland skivorna om fler än 10 skivor finns
function toggle(e) {
    if (e.target.id == 'next-page') {
        for (let i = 0; i < firstPage.length; i++) {
            firstPage[i].style.display = 'none';
        }

        for (let i = 0; i < secondPage.length; i++) {
            secondPage[i].style.display = 'block';
        }
        
        nextLink.style.display = 'none';
        previousLink.style.display = 'block';

    } else if (e.target.id == 'previous-page') {
        for (let i = 0; i < firstPage.length; i++) {
            firstPage[i].style.display = 'block';
        }

        for (let i = 0; i < secondPage.length; i++) {
            secondPage[i].style.display = 'none';
        }
        
        nextLink.style.display = 'block';
        previousLink.style.display = 'none';
    }
}
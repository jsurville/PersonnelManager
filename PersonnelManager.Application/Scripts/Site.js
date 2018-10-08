function Valider(formElement) {
    let form = formElement ? formElement : document.getElementsByTagName('form')[0];

    for (let invalidInput of form.querySelectorAll('.clr-error')) {
        invalidInput.classList.remove('clr-error');
    }

    let estValide = form.checkValidity();
    if (!estValide) {
        for (let invalidInput of form.querySelectorAll(':invalid')) {
            invalidInput.closest('.clr-control-container').classList.add('clr-error');
        }
    }

    return estValide;
}

var agblog = {
    SetLocalStorage: function (key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    },

    GetLocalStorage: function (key) {
        return localStorage.getItem(key);
    },

    DeleteLocalStorage: function (key) {
        localStorage.removeItem(key);
    },

    ClearLocalStorage: function () {
        localStorage.clear();
    },

    CopyToClipboard: function (toCopy) {
        navigator.clipboard.writeText(toCopy)
            .then(() => {
                console.log(toCopy + " copied to clipboard.");
            })
            .catch(err => {
                console.log('Something went wrong', err);
            });
    }
}
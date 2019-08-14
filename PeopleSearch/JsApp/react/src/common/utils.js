/**
 * Format 'data' in specified 'format'
 * @param {string} data
 * @param {string}  format
 * return {string} format applied data
 */
export function formatData(data, format) {
    var today = new Date();
    const date = new Date(data);
    switch (format) {
        case "date":
            if (date instanceof Date && !isNaN(date)) data = Intl.DateTimeFormat().format(date);
            break;
        case "age":
            var age = today.getFullYear() - date.getFullYear();
            var m = today.getMonth() - date.getMonth();
            if (m < 0 || (m === 0 && today.getDate() < date.getDate())) {
                age--;
            }
            data = `${age} y`;
            break;
        default:
            break;
    }

    return data;
}

export function toBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);
    });
}

export const personModel = {
    FirstName: "",
    LastName: "",
    MiddleName: "",
    DateOfBirth: "",
    Picture: "",
    Interests: "",
    Address: {
        AddressLine1: "",
        AddressLine2: "",
        City: "",
        State: "",
        ZipCode: "",
        Country: ""
    }
};

export const requiredPersonKeys = ["FirstName", "LastName", "DateOfBirth", "Picture", "Interests"];
export const requiredAddressKeys = ["AddressLine1", "City", "State", "ZipCode", "Country"];

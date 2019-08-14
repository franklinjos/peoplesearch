import axios from "axios";

export async function search(input) {
    let url = window.location.origin + "/api/people/";
    if (input && input.trim()) {
        url += "search/" + input;
    }
    return await axios.get(url);
}

export async function addPerson(person) {
    let url = window.location.origin + "/api/people/";
    return await axios.post(url, person);
}

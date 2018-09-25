import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

const httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class RoleService {

    constructor(private http: HttpClient) { }

    getRoles() {
        return this.http.get("/api/roles");
    }

    getRole(id) {
        return this.http.get("/api/roles/" + id);
    }

    update(role) {
        var body = JSON.stringify(role);
        return this.http.put("/api/roles/" + role.id, body, httpOptions);
    }

    getUsers(filter) {
        return this.http.get("/api/roles/users?" + this.toQueryString(filter));
    }

    getUser(id) {
        return this.http.get("/api/roles/users/" + id);
    }

    updateUser(user) {
        var body = JSON.stringify(user);
        console.log(user);
        return this.http.put("/api/roles/users/" + user.id, body, httpOptions);
    }

    toQueryString(obj) {
        var parts = [];
        for (var prop in obj) {
            var value = obj[prop];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
        }
        
        return parts.join('&');
    }
}
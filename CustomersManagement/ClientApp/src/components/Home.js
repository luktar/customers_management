import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>Welcome to my interview application</h1>
                <p>To view the list with all customers just press "Customers" button in the upper right corner. You can edit or delete a selected customer by pressing "Edit" and "Delete" buttons. If you want to add a new customer just press on "Add" button next to "Customers".</p>
                <p>The Cities are connected with Customers table with many to many relations.</p>

                <p>Enjoy my application :)</p>

            </div>
        );
    }
}

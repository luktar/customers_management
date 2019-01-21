import React, { Component } from 'react';

export class Customers extends Component {
    static displayName = Customers.name;

    constructor(props) {
        super(props);
        this.state = { customers: [], loading: true };
    }

    getAllCustomers = () => {
        fetch('api/Customers/GetAllAsync')
            .then(response => response.json())
            .then(x => {
                this.setState({ customers: x, loading: false });
            });
    };

    componentDidMount = () => {
        this.getAllCustomers();
    };


    deleteHandler = (id) => {
        if (!window.confirm("Are you sure?"))
            return;
        else {
            fetch('api/Customers/DeleteAsync/' + id, {
                method: 'delete'
            }).then((x) => {
                this.getAllCustomers();
            });
        }
    };


    editHandler = (id) => {
        this.props.history.push("/addcustomer/" + id);
    };

    renderCustomersTable(customers) {
        return (
            <table className='table table-striped'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Surname</th>
                        <th>Email</th>
                        <th>Telephone</th>
                    </tr>
                </thead>
                <tbody>
                    {customers.map(x =>
                        <tr key={x.id}>
                            <td>{x.name}</td>
                            <td>{x.surname}</td>
                            <td>{x.email}</td>
                            <td>{x.telephone}</td>
                            <td>
                                <button className="btn btn-link" onClick={() => this.editHandler(x.id)}>Edit</button>
                                <button className="btn btn-link" onClick={() => this.deleteHandler(x.id)}>Delete</button>
                            </td>  
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCustomersTable(this.state.customers);

        return (
            <div>
                <h1>Customers</h1>
                <p>This table contains all customers.</p>
                {contents}
            </div>
        );
    }
}

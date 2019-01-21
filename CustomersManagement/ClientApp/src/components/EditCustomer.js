import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';

export class EditCustomer extends Component {
    static displayName = EditCustomer.name;

    constructor(props) {
        super(props);

        let edit = true;
        let title = "Edit customer";

        if (!("id" in this.props.match.params)) {
            edit = false;
            title = "Add new customer";
        } else {
            this.customerId = this.props.match.params["id"];
        }

        this.state = {
            title: title,
            loading: true,
            cityList: [],
            customer: {},
            edit: edit
        };
    }

    initCities = () => {

        fetch('api/Cities/GetAllAsync')
            .then(response => response.json())
            .then(data => {
                this.setState({ cityList: data });
            });
    };

    initCustomer = () => {
        if (!this.state.edit) {
            this.setState(
                {
                    customer: {
                        name: "",
                        surname: "",
                        email: "",
                        telephone: "",

                    }
                })
        } else {
            fetch('api/Customers/GetAsync/' + this.customerId)
                .then(r => r.json())
                .then(data => {
                    this.setState({ customer: data });
                });
        }

        this.setState({ loading: false })
    }

    componentDidMount = () => {
        this.initCities();
        this.initCustomer();
    };

    handleSave = (event) => {
        event.preventDefault();

        if (!this.state.edit) {
            fetch('api/Customers/AddAsync', {
                method: "POST",
                body: JSON.stringify(this.state.customer),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                this.props.history.push('/customers');
            });
        } else {
            fetch('api/Customers/UpdateAsync', {
                method: 'PUT',
                body: JSON.stringify(this.state.customer),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then((response) => {
                this.props.history.push("/customers");
            });
        }
    };

    changeHandler = (event) => {
        let customer = { ...this.state.customer };
        customer[event.target.name] = event.target.value;
        this.setState({ customer: customer });
    };

    cancelHandler = (e) => {
        e.preventDefault();
        this.props.history.push("/customers");
    }  

    renderCreateForm = (cityList) => {
        return (
            <form onSubmit={this.handleSave} >
                <FormGroup >
                    <Label >Name</Label>
                    <Input type="text" name="name" placeholder="Enter customer name..." value={this.state.customer.name} onChange={this.changeHandler} required />
                </FormGroup>
                <FormGroup >
                    <Label >Surname</Label>
                    <Input type="text" name="surname" placeholder="Enter customer surname..." value={this.state.customer.surname} onChange={this.changeHandler} required />
                </FormGroup>
                <FormGroup >
                    <Label >Email</Label>
                    <Input type="email" name="email" placeholder="Enter customer email..." value={this.state.customer.email} onChange={this.changeHandler} required />
                </FormGroup >
                <FormGroup >
                    <Label >Telephone</Label>
                    <Input type="text" name="telephone" placeholder="Enter customer telephone..." value={this.state.customer.telephone} onChange={this.changeHandler} />
                </FormGroup >
                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.cancelHandler}>Cancel</button>
                </div >
            </form >
        )
    };

    render = () => {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.cityList);

        return <div className="col-md-6">
            <h1>{this.state.title}</h1>
            <hr />
            {contents}
        </div>;
    };
}

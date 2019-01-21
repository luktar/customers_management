import React, { Component } from 'react';

export class EditCustomer extends Component {
    static displayName = EditCustomer.name;

    constructor(props) {
        super(props);
        this.state = {
            title: "", loading: true, cityList: [], customer: {}
        };

        this.customerId = this.props.match.params["id"];
    }

    initCities = () => {

        fetch('api/Cities/GetAllAsync')
            .then(response => response.json())
            .then(data => {
                this.setState({ cityList: data });
            });

    };

    initCustomer = () => {
        fetch('api/Customers/GetAsync/' + this.customerId)
            .then(r => r.json())
            .then(data => {
                this.setState({ customer: data, loading: false });
            });
    }

    componentDidMount = () => {
        this.initCities();
        this.initCustomer();
    };

    handleSave = (event) => {
        event.preventDefault();
        const data = new FormData(event.target);
        console.log(data);
        //if (this.state.empData.employeeId) {
        //    fetch('api/Customers/Update', {
        //        method: 'PUT',
        //        body: data,
        //    }).then((response) => response.json())
        //        .then((responseJson) => {
        //            this.props.history.push("/fetchemployee");
        //        })
        //}
        //else {
        //    fetch('api/Employee/Create', {
        //        method: 'POST',
        //        body: data,
        //    }).then((response) => response.json())
        //        .then((responseJson) => {
        //            this.props.history.push("/fetchemployee");
        //        })
        //}
    };

    handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    };

    renderCreateForm = (cityList) => {
        return (
            <form onSubmit={this.handleSave} >
                <div className="form-group row" >
                    <input type="hidden" name="id" value={this.state.customer.id} />
                </div>
                < div className="form-group row" >
                    <label className=" control-label col-md-12" htmlFor="Name">Name</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="name" onChange={this.handleChange} value = { this.state.customer.name } required />
                    </div>
                </div >
                <div className="form-group row">
                    <label className="control-label col-md-12" htmlFor="Surname" >Surname</label>
                    <div className="col-md-4">
                        <input className="form-control" type="text" name="surname" onChange={this.handleChange} value={this.state.customer.surname} required />
                    </div>
                </div>

                <div className="form-group">
                    <button type="submit" className="btn btn-default">Save</button>
                    <button className="btn" onClick={this.handleCancel}>Cancel</button>
                </div >
            </form >
        )
    };

    render = () => {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderCreateForm(this.state.cityList);

        return <div>
            <h1>{this.state.title}</h1>
            <h3>Customer data</h3>
            <hr />
            {contents}
        </div>;
    };
}

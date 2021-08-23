import React, { Component } from 'react';
import { matchPath } from 'react-router';

export class HospitalDelete extends Component {
    static displayName = HospitalDelete.name;

  constructor(props) {
    super(props);
      this.state = {
          hospital: {},
          loading : true
      };
      var m2 = matchPath(props.location.pathname, { path: "/hospital-delete/:id", exact: false, strict: false })
      if (m2 && m2.params.id)
          this.state.hospital.id = m2.params.id;

      this.submitForm = this.submitForm.bind(this);
      //this.handleSubmit = this.handleSubmit.bind(this);
  }

    componentDidMount() {
        this.getHospital(this.state.hospital.id);
    }

    cancel() {
        window.location.href = "../hospital-data/";
    }

    submitForm() {
        //this.formRef.dispatchEvent(new Event("submit", { bubbles: true, cancelable: true }));
        this.deleteHospital(this.state.hospital);
    };

    render()
    {
        if (this.state.loading)
            return <p><em>Loading...</em></p>;
    return (
      <div>
            <h1>{this.state.hospital.title}</h1>
            <h2>will be irreversibly deleted.</h2>
            <h3>Select "Delete" to continue deleting or "Cancel" to go back.</h3>
            <p></p>
            <button onClick={this.submitForm}>Delete</button>
            <button onClick={this.cancel}>Cancel</button>
      </div>
    );
    };

    async deleteHospital(hospital) {
        const response = await
            fetch('hospital/delete/',
                {
                    method: "post",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(hospital)
                });
        const data = await response.json();
        this.cancel();
    };

    async getHospital(hospitalId) {
        const response = await fetch('hospital/Read?hospitalId=' + hospitalId);
        const data = await response.json();
        this.setState({ hospital : data, loading : false });
    };
}

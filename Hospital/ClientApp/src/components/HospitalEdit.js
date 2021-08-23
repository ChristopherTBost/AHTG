import React, { Component } from 'react';
import { matchPath } from 'react-router';

export class HospitalEdit extends Component {
    static displayName = HospitalEdit.name;

  constructor(props) {
    super(props);
      this.state = {
          hospital: {},
          loading : true
      };
      var m2 = matchPath(props.location.pathname, { path: "/hospital-edit/:id", exact: false, strict: false })
      if (m2 && m2.params.id)
          this.state.hospital.id = m2.params.id;

      this.handleChange = this.handleChange.bind(this);
      this.submitForm = this.submitForm.bind(this);
      //this.handleSubmit = this.handleSubmit.bind(this);
  }

    componentDidMount() {
        this.getHospital(this.state.hospital.id);
    }

    handleChange(event) {
        //this.state.hospital.title = event.target.value;
        var h = this.state.hospital;
        h.title = event.target.value;
        this.setState({ hospital: h });
    };

    handleSubmit(event)
    {
        event.preventDefault();
        window.location.href = "/hospital-data";
    };

    cancel() {
        window.location.href = "../hospital-data/";
    }

    submitForm() {
        //this.formRef.dispatchEvent(new Event("submit", { bubbles: true, cancelable: true }));
        this.updateHospital(this.state.hospital);
    };

    render()
    {
        if (this.state.loading)
            return <p><em>Loading...</em></p>;
    return (
      <div>
            <h1>{this.state.hospital.title}</h1>

            <form ref={ref => this.formRef = ref} method="post" action='../hospital/update/' onSubmit={this.handleSubmit}>
                <label htmlFor="title">Title</label>
                <input id="title" name="title" type="text" onChange={this.handleChange} defaultValue={this.state.hospital.title} />
                <input id="id" name="id" type="hidden" defaultValue={this.state.hospital.id} />
                <p></p>
            </form>
            <button onClick={this.submitForm}>Save</button>
            <button onClick={this.cancel}>Cancel</button>
      </div>
    );
    };

    async updateHospital(hospital) {
        const response = await
            fetch('hospital/update/',
                {
                    method: "post",
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(hospital)
                });
        const data = await response.json();
        this.setState({ hospital: data, loading: false });
        this.cancel();
        //window.location.href = "../hospital-data/";
    };

    async getHospital(hospitalId) {
        const response = await fetch('hospital/Read?hospitalId=' + hospitalId);
        const data = await response.json();
        this.setState({ hospital : data, loading : false });
    };
}

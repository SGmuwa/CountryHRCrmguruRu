import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as GetCountryStore from '../store/GetCountry';

// At runtime, Redux will merge together...
type GetCountryProps =
  GetCountryStore.GetCountryState // ... state we've requested from the Redux store
  & typeof GetCountryStore.actionCreators // ... plus action creators we've requested


class GetCountry extends React.PureComponent<GetCountryProps> {
  // This method is called when the component is first added to the document
  public componentDidMount() {
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">Country info</h1>
        <p>This component demonstrates fetching data from the server and working with URL parameters.</p>
        {this.renderInput()}
        {this.renderForecastsTable()}
      </React.Fragment>
    );
  }

  handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target;

    this.setState({
      [name]: value
    });
  };

  submit = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.keyCode == 13) {
      const inputName = this.props.inputName;
      var esc = encodeURIComponent;
      const uri = 'https://restcountries.eu/rest/v2/name/' + esc(this.props.inputName);
      fetch(uri)
        .then(response => response.json() as Promise<GetCountryStore.CountryInfo[]>)
        .then(data => {
          dispatch({ type: 'RECEIVE_WEATHER_FORECASTS', startDateIndex: startDateIndex, forecasts: data });
      });
    }
  };

  private renderInput(): React.ReactNode {
    return (
      <React.Fragment>
        <text>Input: </text>
        <input type="text" name="inputName" value={this.props.inputName} onChange={this.handleChange} onKeyPress={this.submit} />
      </React.Fragment>
    )
  }

  private renderForecastsTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Code</th>
            <th>Capital</th>
            <th>Area</th>
            <th>Population</th>
            <th>Region</th>
          </tr>
        </thead>
        <tbody>
          {this.props.forecasts.map((forecast: GetCountryStore.CountryInfo) =>
            <tr key={forecast.name}>
              <td>{forecast.name}</td>
              <td>{forecast.code}</td>
              <td>{forecast.capital}</td>
              <td>{forecast.area}</td>
              <td>{forecast.population}</td>
              <td>{forecast.region}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.getCountry, // Selects which state properties are merged into the component's props
  GetCountryStore.actionCreators // Selects which action creators are merged into the component's props
)(GetCountry as any);

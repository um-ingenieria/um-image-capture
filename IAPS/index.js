const sql = require('mssql')
const csv = require('csv-parser')
const fs = require('fs');
const { table } = require('console');
const results = [];

const fileName = './female-subjects.txt';
const tableName = 'IAPS_FEMALE_SUBJECTS'

/**
* 
* 
* A los archicos de IAPS hay que borrarles todo el texto raro que tienen arriba (menos los nombres de las columnas)
* Y las \ al final de cada linea (con un find and replace..)
* 
* 
*/

class IAPSImageData {
  constructor(rawData) {
    this.description = rawData.desc,
    this.id_iaps = rawData.IAPS,
    this.valence_mean = rawData.valmn,
    this.valence_standard_deviation = rawData.valsd,
    this.arousal_mean = rawData.aromn,
    this.arousal_standard_deviation = rawData.arosd,
    this.dominance_1_mean = rawData.dom1mn === '.' ? null : rawData.dom1mn,
    this.dominance_1_standard_deviation = rawData.dom1sd === '.' ? null : rawData.dom1sd,
    this.dominance_2_mean = rawData.dom2mn === '.' ? null : rawData.dom2mn,
    this.dominance_2_standard_deviation = rawData.dom2sd === '.' ? null : rawData.dom2sd,
    this.set_id = rawData.set
  }
}

const saveResultsToDatabase = async (listOfIAPSImageData) => {
  console.info('Saving data to database')
  const pool = await sql.connect('mssql://sa:polopolo9@localhost/UM_NEUROSKY')
  
  if (!pool) throw new Error('Could not get connection pool');
  
  const transaction = new sql.Transaction(pool)
  
  if (!pool) throw new Error('Could not init transacion');
  
  let rolledBack = false
  
  transaction.on('rollback', () => {
    rolledBack = true 
    throw new Error('TRANSACION ROLLBACKED SUCCESSFULY')
  })
  
  await transaction.begin()

  try {
    let queryString

    const insertNextRow = async (listOfIAPSImageData) => {
      if (listOfIAPSImageData.length === 0) {
        await transaction.commit()
        console.info('[TRANSACTION COMMITED] All rows were inserted')
        return;
      }

      const request = transaction.request();

      const anIAPSImageData = listOfIAPSImageData.pop();

      console.log(`Inserting data of picture ${anIAPSImageData.id_iaps}`);

      const { description, id_iaps, valence_mean, valence_standard_deviation, arousal_mean, arousal_standard_deviation, dominance_1_mean, dominance_1_standard_deviation, dominance_2_mean, dominance_2_standard_deviation, set_id } = anIAPSImageData;
      
      queryString = `INSERT INTO ${tableName} VALUES ('${description}', ${id_iaps}, ${valence_mean}, ${valence_standard_deviation}, ${arousal_mean}, ${arousal_standard_deviation}, ${dominance_1_mean}, ${dominance_1_standard_deviation}, ${dominance_2_mean}, ${dominance_2_standard_deviation}, ${set_id})`

      try {
        request.query(queryString, (err) => {
          if (err) {
            console.log(queryString)
            throw err;
          }
          console.log('Calling next with', listOfIAPSImageData)
          insertNextRow(listOfIAPSImageData)
        });
      } catch (error) {
        console.log('ERRORRRRRR', error)
        throw error
      }
    }

    insertNextRow(listOfIAPSImageData)
  } catch(err) {
    if (!rolledBack) {
      await transaction.rollback()
      console.error('[TRANSACTION ROLLBACKED] because an error occured')
    }
    throw err
  }
}

(async () => {
  try {
    fs.createReadStream(fileName)
    .pipe(csv({ separator: '\t' }))
    .on('data', (data) => results.push(data))
    .on('end', async () => {
      // Print raw results
      console.log(results)
    
      // Build a list of objets with the parsed data of each image
      const listOfIAPSImageData = results.map(rawData => new IAPSImageData(rawData))
    
      await saveResultsToDatabase(listOfIAPSImageData)
    });
  } catch (error) {
    console.error('AN ERROR OCCURED', error.message)
  }
})()
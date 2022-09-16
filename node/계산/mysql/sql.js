module.exports = {
    customerlist : `select * from new_table`,
    customerInsert : `insert into new_table set ?`,
    customerUpdate : `update new_table set ? where id=?`,
    customerDelete : `delete from new_table where id=?`
}